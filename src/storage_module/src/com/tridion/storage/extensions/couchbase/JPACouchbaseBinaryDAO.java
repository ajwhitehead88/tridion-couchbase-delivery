package com.tridion.storage.extensions.couchbase;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import com.tridion.broker.StorageException;
import com.tridion.storage.BinaryContent;
import com.tridion.storage.extensions.couchbase.couchbase.CouchbaseManager;
import com.tridion.storage.extensions.couchbase.json.JsonHelper;
import com.tridion.storage.persistence.JPABinaryContentDAO;

@Component("JPACouchbaseBinaryDAO")
@Scope("prototype")
public class JPACouchbaseBinaryDAO extends JPABinaryContentDAO
{
	private static final String BINARY_FORMAT = "Binary_%s_%s";
	private static final Logger LOG = LoggerFactory.getLogger(JPACouchbaseBinaryDAO.class);

	public JPACouchbaseBinaryDAO(String storageId, EntityManagerFactory entityManagerFactory, EntityManager entityManager, String storageName)
	{
		super(storageId, entityManagerFactory, entityManager, storageName);
		LOG.debug("Initialising couchbase binary indexer");
	}
	
	public JPACouchbaseBinaryDAO(String storageId, EntityManagerFactory entityManagerFactory, String storageName)
    {
		super(storageId, entityManagerFactory, storageName);
		LOG.debug("Initialising couchbase binary indexer");
	}
	
	/**
	 * Create indexes the binary content into Couchbase
	 */
	@Override
	public void create(BinaryContent binaryContent, String relativePath) throws StorageException
	{
		// Call super create to make sure the normal process is completed first
		super.create(binaryContent, relativePath);

		LOG.debug("Instantiating the couchbase manager");
		
		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String id = String.format(BINARY_FORMAT, binaryContent.getPublicationId(), binaryContent.getBinaryId());
            LOG.debug("Adding binary with id " + id);
			manager.set(JsonHelper.createBinaryJson(id, binaryContent, relativePath));
            LOG.debug("Successfully added binary with id " + id);
		}
		catch (Exception e)
		{
			LOG.error("Error indexing to couchbase", e);
			throw new StorageException("Error indexing to couchbase", e);
		}
	}

	/**
	 * Update indexes the binary content into Couchbase
	 */
	@Override
	public void update(BinaryContent binaryContent, String originalRelativePath, String newRelativePath) throws StorageException
	{
		// Call super update to make sure the normal process is completed first
		super.update(binaryContent, originalRelativePath, newRelativePath);

		LOG.debug("Instantiating the couchbase manager");
		
		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String id = String.format(BINARY_FORMAT, binaryContent.getPublicationId(), binaryContent.getBinaryId());
            LOG.debug("Updating binary with id " + id);
			manager.set(JsonHelper.createBinaryJson(id, binaryContent, newRelativePath));
			LOG.debug("Successfully updated binary with id " + id);
		}
		catch (Exception e)
		{
			LOG.error("Error removing binary from couchbase", e);
			throw new StorageException("Error removing binary from couchbase", e);
		}
	}

	/**
	 * Removes an item from the index on unpublish based on the identifiers
	 */
	@Override
	public void remove(int publicationId, int binaryId, String variantId, String relativePath) throws StorageException
	{
        // Call super remove to make sure the normal process is completed first
        super.remove(publicationId, binaryId, variantId, relativePath);

		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String id = String.format(BINARY_FORMAT, publicationId, binaryId);
            LOG.debug("Removing binary with id " + id);
			manager.delete(id);
			LOG.debug("Successfully deleted binary with id " + id);
		}
		catch (Exception e)
		{
			LOG.error("Error removing binary from couchbase", e);
			throw new StorageException("Error removing binary from couchbase", e);
		}
	}
}

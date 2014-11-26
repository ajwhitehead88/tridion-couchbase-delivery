package com.tridion.storage.extensions.couchbase;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import com.couchbase.client.java.document.JsonStringDocument;
import com.tridion.broker.StorageException;
import com.tridion.data.CharacterData;
import com.tridion.storage.extensions.couchbase.couchbase.CouchbaseManager;
import com.tridion.storage.persistence.JPAPageDAO;

@Component("JPACouchbasePageDAO")
@Scope("prototype")
public class JPACouchbasePageDAO extends JPAPageDAO
{
	private static final String KEY_FORMAT = "tcm:%s-%s-64";
	private static final Logger LOG = LoggerFactory.getLogger(JPACouchbasePageDAO.class);

	public JPACouchbasePageDAO(String storageId, EntityManagerFactory entityManagerFactory, EntityManager entityManager, String storageName)
	{
		super(storageId, entityManagerFactory, entityManager, storageName);
		LOG.debug("Initialising couchbase page indexer");
	}
	
	public JPACouchbasePageDAO(String storageId, EntityManagerFactory entityManagerFactory, String storageName)
	{
		super(storageId, entityManagerFactory, storageName);
		LOG.debug("Initialising couchbase page indexer");
	}
	
	/**
	 * Create indexes the itinerary content into Couchbase
	 */
	@Override
	public void create(CharacterData page, String relativePath) throws StorageException
	{
		// Call super create to make sure the normal process is completed first
		super.create(page, relativePath);

		LOG.debug("Instantiating the couchbase manager");
		
		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String key = String.format(KEY_FORMAT, page.getPublicationId(), page.getId());
			manager.set(JsonStringDocument.create(key, page.getString()));
			LOG.debug("Successfully added document with key " + key);
		}
		catch (Exception e)
		{
			LOG.error("Error indexing to couchbase", e);
			throw new StorageException("Error indexing to couchbase", e);
		}
	}

	@Override
	public void update(CharacterData page, String originalRelativePath, String newRelativePath) throws StorageException
	{
		// Call super update to make sure the normal process is completed first
		super.update(page, originalRelativePath, newRelativePath);

		LOG.debug("Instantiating the couchbase manager");
		
		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String key = String.format(KEY_FORMAT, page.getPublicationId(), page.getId());
			manager.set(JsonStringDocument.create(key, page.getString()));
			LOG.debug("Successfully updated document with key " + key);
		}
		catch (Exception e)
		{
			LOG.error("Error removing document from couchbase", e);
			throw new StorageException("Error removing document from couchbase", e);
		}
	}

	/**
	 * Removes an item from the index on unpublish based on the identifiers
	 */
	@Override
	public void remove(int publicationId, int pageId, String relativePath) throws StorageException
	{
        // Call super remove to make sure the normal process is completed first
        super.remove(publicationId, pageId, relativePath);

		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String key = String.format(KEY_FORMAT, publicationId, pageId);
			manager.delete(key);
			LOG.debug("Successfully deleted document with key " + key);
		}
		catch (Exception e)
		{
			LOG.error("Error removing document from couchbase", e);
			throw new StorageException("Error removing document from couchbase", e);
		}
	}
}

package com.tridion.storage.extensions.couchbase;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import com.couchbase.client.java.document.JsonStringDocument;
import com.tridion.broker.StorageException;
import com.tridion.storage.ComponentPresentation;
import com.tridion.storage.extensions.couchbase.couchbase.CouchbaseManager;
import com.tridion.storage.persistence.JPAComponentPresentationDAO;
import com.tridion.storage.util.ComponentPresentationTypeEnum;

@Component("JPACouchbasePageDAO.java")
@Scope("prototype")
public class JPACouchbasePresentationDAO extends JPAComponentPresentationDAO
{
	private static final String KEY_FORMAT = "tcm:%s-%s-16_tcm:%s-%s-32";
	private static final Logger LOG = LoggerFactory.getLogger(JPACouchbasePresentationDAO.class);

	public JPACouchbasePresentationDAO(String storageId, EntityManagerFactory entityManagerFactory, EntityManager entityManager, String storageName)
	{
		super(storageId, entityManagerFactory, entityManager, storageName);
		LOG.debug("Initialising couchbase presentation indexer");
	}
	
	public JPACouchbasePresentationDAO(String storageId, EntityManagerFactory entityManagerFactory, String storageName)
	{
		super(storageId, entityManagerFactory, storageName);
		LOG.debug("Initialising couchbase presentation indexer");
	}
	
	/**
	 * Create indexes the itinerary content into Couchbase
	 */
	@Override
	public void create(ComponentPresentation itemToUpdate, ComponentPresentationTypeEnum componentPresentationType) throws StorageException
	{
		// Call super create to make sure the normal process is completed first
		super.create(itemToUpdate, componentPresentationType);

		LOG.debug("Instantiating the couchbase manager");
		
		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String key = String.format(KEY_FORMAT,
					itemToUpdate.getPublicationId(),
					itemToUpdate.getComponentId(),
					itemToUpdate.getPublicationId(),
					itemToUpdate.getTemplateId());
			manager.set(JsonStringDocument.create(key, new String(itemToUpdate.getContent())));
			LOG.debug("Successfully added document with key " + key);
		}
		catch (Exception e)
		{
			LOG.error("Error indexing to couchbase", e);
			throw new StorageException("Error indexing to couchbase", e);
		}
	}

	@Override
	public void update(ComponentPresentation itemToUpdate, ComponentPresentationTypeEnum componentPresentationType) throws StorageException
	{
		// Call super update to make sure the normal process is completed first
		super.update(itemToUpdate, componentPresentationType);

		LOG.debug("Instantiating the couchbase manager");
		
		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String key = String.format(KEY_FORMAT,
					itemToUpdate.getPublicationId(),
					itemToUpdate.getComponentId(),
					itemToUpdate.getPublicationId(),
					itemToUpdate.getTemplateId());
			manager.set(JsonStringDocument.create(key, new String(itemToUpdate.getContent())));
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
	public void remove(ComponentPresentation presentation, ComponentPresentationTypeEnum componentPresentationType) throws StorageException
	{
        // Call super remove to make sure the normal process is completed first
        super.remove(presentation, componentPresentationType);

		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String key = String.format(KEY_FORMAT,
					presentation.getPublicationId(),
					presentation.getComponentId(),
					presentation.getPublicationId(),
					presentation.getTemplateId());
			manager.delete(key);
			LOG.debug("Successfully deleted document with key " + key);
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
	public void remove(int publicationId, int componentId, int componentTemplateId, ComponentPresentationTypeEnum componentPresentationType) throws StorageException
	{
        // Call super remove to make sure the normal process is completed first
		super.remove(publicationId, componentId, componentTemplateId, componentPresentationType);

		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String key = String.format(KEY_FORMAT,
					publicationId,
					componentId,
					publicationId,
					componentTemplateId);
			manager.delete(key);
		}
		catch (Exception e)
		{
			LOG.error("Error removing document from couchbase", e);
			throw new StorageException("Error removing document from couchbase", e);
		}
	}
}

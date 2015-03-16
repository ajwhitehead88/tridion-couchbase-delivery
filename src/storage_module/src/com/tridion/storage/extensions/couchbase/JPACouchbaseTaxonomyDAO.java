package com.tridion.storage.extensions.couchbase;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import com.tridion.broker.StorageException;
import com.tridion.storage.KeywordRelation;
import com.tridion.storage.TaxonomyItem;
import com.tridion.storage.extensions.couchbase.couchbase.CouchbaseManager;
import com.tridion.storage.extensions.couchbase.json.JsonHelper;
import com.tridion.storage.persistence.JPATaxonomyDAO;

@Component("JPACouchbaseTaxonomyDAO")
@Scope("prototype")
public class JPACouchbaseTaxonomyDAO extends JPATaxonomyDAO
{
	private static final String TAXONOMY_FORMAT = "Taxonomy_%s_%s_%s";
	private static final Logger LOG = LoggerFactory.getLogger(JPACouchbaseTaxonomyDAO.class);

	public JPACouchbaseTaxonomyDAO(String storageId, EntityManagerFactory entityManagerFactory, EntityManager entityManager, String storageName)
	{
		super(storageId, entityManagerFactory, entityManager, storageName);
		LOG.debug("Initialising couchbase taxonomy indexer");
	}
	
	public JPACouchbaseTaxonomyDAO(String storageId, EntityManagerFactory entityManagerFactory, String storageName)
    {
		super(storageId, entityManagerFactory, storageName);
		LOG.debug("Initialising couchbase taxonomy indexer");
	}
	
	/**
	 * Create indexes the taxonomy content into Couchbase
	 */
	@Override
	public TaxonomyItem store(TaxonomyItem taxonomyItem) throws StorageException
	{
		//TODO
		LOG.debug("Instantiating the couchbase manager");
		
		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String id = String.format(TAXONOMY_FORMAT, taxonomyItem.getPublicationId(), taxonomyItem.getId(), taxonomyItem.getItemType());
            LOG.debug("Adding taxonomy with id " + id);
			manager.set(JsonHelper.createCategoryJson(id, taxonomyItem));
            LOG.debug("Successfully added taxonomy with id " + id);
		}
		catch (Exception e)
		{
			LOG.error("Error indexing taxonomy to couchbase", e);
			throw new StorageException("Error indexing taxonomy to couchbase", e);
		}

		// Call super create to make sure the normal process is completed last
		return super.create(taxonomyItem);
	}

	/**
	 * Stores a keyword relation
	 */
	@Override
	public KeywordRelation store(KeywordRelation keywordRelation) throws StorageException
	{
		//TODO: Implement keyword relations?
		return super.store(keywordRelation);
	}

	/**
	 * Removes an item from the index on unpublish based on the identifiers
	 */
	@Override
	public void remove(int publicationId, int taxonomyId) throws StorageException
	{
		//TODO
        // Call super remove to make sure the normal process is completed first
        super.remove(publicationId, taxonomyId);

		try (CouchbaseManager manager = new CouchbaseManager())
		{
			String id = String.format(TAXONOMY_FORMAT, publicationId, taxonomyId);
            LOG.debug("Removing taxonomy with id " + id);
			manager.delete(id);
			LOG.debug("Successfully deleted taxonomy with id " + id);
		}
		catch (Exception e)
		{
			LOG.error("Error removing taxonomy from couchbase", e);
			throw new StorageException("Error removing taxonomy from couchbase", e);
		}
	}
}

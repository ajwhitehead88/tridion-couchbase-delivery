package com.tridion.storage.extensions.couchbase.couchbase;

import java.io.IOException;
import java.util.List;
import java.util.concurrent.ExecutionException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.couchbase.client.java.Bucket;
import com.couchbase.client.java.Cluster;
import com.couchbase.client.java.CouchbaseCluster;
import com.couchbase.client.java.document.JsonDocument;
import com.couchbase.client.java.document.JsonStringDocument;
import com.tridion.storage.extensions.couchbase.configuration.CouchbaseConfiguration;

/**
 * Couchbase client for managing setting and deleting JSON objects 
 */
public class CouchbaseManager implements AutoCloseable
{
	private static final Logger LOG = LoggerFactory.getLogger(CouchbaseManager.class);
	
	private Cluster cluster;
	private Bucket bucket;
		
	public CouchbaseManager() throws IOException
	{
	    // Connect to the Cluster
	    List<String> hosts = CouchbaseConfiguration.getCouchbaseServers();
	    String bucketName = CouchbaseConfiguration.getCouchbaseBucketName();
	    
	    cluster = CouchbaseCluster.create(hosts);
	    bucket = cluster.openBucket(bucketName);
	    
	    LOG.debug("Couchbase client created");
	}
	
	@Override
	public void close() throws Exception
	{
	    // Shutting down properly
		cluster.disconnect();
	    LOG.debug("Couchbase client shutdown");
	}
	
	/**
	 * Sets a single document
	 * @param key
	 * @param document
	 * @throws ExecutionException 
	 * @throws InterruptedException 
	 */
	public void set(JsonDocument document) throws InterruptedException, ExecutionException
	{
        LOG.debug("Setting document in DB with key: " + document.id());
        
        // Set the document
        bucket.upsert(document);
	}

	/**
	 * Sets a single document
	 * @param key
	 * @param document
	 * @throws ExecutionException 
	 * @throws InterruptedException 
	 */
	public void set(JsonStringDocument document) throws InterruptedException, ExecutionException
	{
        LOG.debug("Setting document in DB with key: " + document.id());
        
        // Set the document
        bucket.upsert(document);
	}	

	/**
	 * Deletes a document out of Couchbase
	 * @param id
	 * @throws InterruptedException
	 * @throws ExecutionException
	 */
	public void delete(String id) throws InterruptedException, ExecutionException
	{
		// Delete the document by id
		bucket.remove(id);
	}
}

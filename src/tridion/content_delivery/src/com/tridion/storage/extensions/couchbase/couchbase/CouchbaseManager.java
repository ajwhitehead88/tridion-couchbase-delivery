package com.tridion.storage.extensions.couchbase.couchbase;

import java.io.IOException;
import java.net.URI;
import java.util.List;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;

import net.spy.memcached.internal.OperationFuture;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.couchbase.client.CouchbaseClient;
import com.couchbase.client.CouchbaseConnectionFactoryBuilder;
import com.tridion.storage.extensions.couchbase.configuration.CouchbaseConfiguration;
import com.tridion.storage.extensions.couchbase.couchbase.transcoders.NetJsonTranscoder;

/**
 * Couchbase client for managing setting and deleting JSON objects 
 */
public class CouchbaseManager implements AutoCloseable
{
	private static final Logger LOG = LoggerFactory.getLogger(CouchbaseManager.class);
	
	private CouchbaseClient client;
		
	public CouchbaseManager() throws IOException
	{
	    // Connect to the Cluster
	    List<URI> hosts = CouchbaseConfiguration.getCouchbaseServers();
	    String bucket = CouchbaseConfiguration.getCouchbaseBucketName();
	    String password = "";
	    
	    CouchbaseConnectionFactoryBuilder cfb = new CouchbaseConnectionFactoryBuilder();
	    cfb.setTranscoder(new NetJsonTranscoder());
	    cfb.setOpTimeout(10000);
	    client = new CouchbaseClient(cfb.buildCouchbaseConnection(hosts, bucket, password));
	    
	    LOG.debug("Couchbase client created");
	}
	
	@Override
	public void close() throws Exception
	{
	    // Shutting down properly
		client.shutdown(60, TimeUnit.SECONDS);
	    LOG.debug("Couchbase client shutdown");
	}
	
	/**
	 * Sets a single document
	 * @param key
	 * @param document
	 * @throws ExecutionException 
	 * @throws InterruptedException 
	 */
	public boolean set(String key, String document) throws InterruptedException, ExecutionException
	{
        LOG.debug("Setting document in DB with key: " + key);
        
        // Set the document
        OperationFuture<Boolean> operation = client.set(key, document);

        // Get the document to block the thread (simulate synchronous set)
        operation.get();
        
        // Return the result
        return operation.isDone();
	}
	
	/**
	 * Deletes a document out of Couchbase
	 * @param key
	 * @return
	 * @throws InterruptedException
	 * @throws ExecutionException
	 */
	public boolean delete(String key) throws InterruptedException, ExecutionException
	{
		// Delete the document by key
		OperationFuture<Boolean> operation = client.delete(key);
		
        // Get the document to block the thread (simulate synchronous set)
        operation.get();
        
        // Return the result
        return operation.isDone();
	}
}

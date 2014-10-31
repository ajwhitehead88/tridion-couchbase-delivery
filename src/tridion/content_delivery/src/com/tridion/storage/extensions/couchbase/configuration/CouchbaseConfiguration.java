package com.tridion.storage.extensions.couchbase.configuration;

import java.io.IOException;
import java.io.InputStream;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.tridion.configuration.Configuration;
import com.tridion.configuration.ConfigurationException;
import com.tridion.configuration.XMLConfigurationReader;

import org.xml.sax.InputSource;

public class CouchbaseConfiguration
{
	private static Configuration config;

	/**
	 * Get the configuration from the Bundle.xml
	 * @return
	 */
	private static Configuration getConfig()
	{
		if (config == null)
		{
			// Read the xml from the Bundle
			InputStream inputStream = CouchbaseConfiguration.class.getClassLoader().getResourceAsStream("CouchbaseDAOBundle.xml");
			try
			{
				// Read the config xml into a Configuration object
				config = new XMLConfigurationReader().readConfiguration(new InputSource(inputStream));
			}
			catch (ConfigurationException e)
			{
				throw new RuntimeException("Error reading bundle configuration", e);
			}
			finally
			{
				try
				{
					inputStream.close();
				}
				catch (IOException e)
				{
					throw new RuntimeException("Error reading bundle configuration", e);
				}
			}
		}
		
		return config;
	}
	
	/**
	 * Get the couchbase bucket name from the bundle XML configuration
	 * @return
	 */
	public static String getCouchbaseBucketName()
	{
		try
		{
			return getConfig().getChild("couchbase").getChild("servers").getAttribute("bucket");
		}
		catch (ConfigurationException e)
		{
			throw new RuntimeException("Error reading couchbase bucket from bundle configuration", e);
		}
	}
	
	/**
	 * Get the couchbase bucket servers 
	 * @return
	 */
	public static List<URI> getCouchbaseServers()
	{
		try
		{
			List<URI> uris = new ArrayList<URI>(); 
			for (Configuration node : getConfig().getChild("couchbase").getChild("servers").getChildrenByName("add"))
			{
				uris.add(new URI(node.getAttribute("uri")));
			}
			
			return uris;
		}
		catch (ConfigurationException e)
		{
			throw new RuntimeException("Error reading couchbase servers from bundle configuration", e);
		}
		catch (URISyntaxException e)
		{
			throw new RuntimeException("Error parsing couchbase servers from bundle configuration", e);
		}
	}
}
package com.tridion.storage.extensions.couchbase.json;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.TimeZone;

import org.apache.commons.codec.binary.Base64;

import com.couchbase.client.java.document.JsonDocument;
import com.couchbase.client.java.document.json.JsonObject;
import com.tridion.storage.BinaryContent;
import com.tridion.storage.Keyword;
import com.tridion.storage.TaxonomyItem;

public class JsonHelper
{
	/**
	 * Creates JSON document for binary content
	 * @param id
	 * @param binaryContent
	 * @param relativePath
	 * @return
	 */
	public static JsonDocument createBinaryJson(String id, BinaryContent binaryContent, String relativePath)
	{
        DateFormat df = getDateFormat();
		String tcmid = String.format("tcm:%s-%s-16", binaryContent.getPublicationId(), binaryContent.getBinaryId());
		return JsonDocument.create(id, JsonObject.empty()
                                                 .put("tcmUri", tcmid)
                                                 .put("publicationId", binaryContent.getPublicationId())
                                                 .put("publishedUrl", relativePath)
                                                 .put("content", Base64.encodeBase64String(binaryContent.getContent()))
                                                 .put("publishDate", df.format(new Date())));
	}

	/**
	 * Creates JSON document for Taxonomy
	 * @param tcmid
	 * @param taxonomyItem
	 * @return
	 */
	public static JsonDocument createCategoryJson(String id, TaxonomyItem taxonomyItem)
	{
		//TODO
		Keyword item = (Keyword) taxonomyItem;
        DateFormat df = getDateFormat();
		String tcmid = String.format("tcm:%s-%s-%s", item.getPublicationId(), item.getId(), item.getItemType());
		JsonObject obj = JsonObject.empty()
                                   .put("tcmUri", tcmid)
                                   .put("publicationId", item.getPublicationId())
                                   .put("key", item.getKey())
                                   .put("parent", item.getParent())
                                   .put("name", item.getName())
                                   .put("description", item.getDescription())
                                   .put("publishDate", df.format(new Date()));

		return JsonDocument.create(id, obj);
	}

	/**
	 * Get the date format
	 * @return
	 */
	private static DateFormat getDateFormat()
	{
		TimeZone tz = TimeZone.getTimeZone("UTC");
        DateFormat df = new SimpleDateFormat("yyyy-MM-dd'T'HH:mmZ");
		df.setTimeZone(tz);
		return df;
	}
}

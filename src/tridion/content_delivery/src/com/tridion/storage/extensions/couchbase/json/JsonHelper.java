package com.tridion.storage.extensions.couchbase.json;

import java.util.Calendar;

import org.apache.commons.codec.binary.Base64;

import com.couchbase.client.java.document.json.JsonObject;
import com.tridion.storage.BinaryContent;

public class JsonHelper
{
	public static JsonObject createJson(BinaryContent binaryContent, String relativePath)
	{
		return JsonObject.empty()
		.put("tcmUri", String.format("tcm:%s-%s-16", binaryContent.getPublicationId(), binaryContent.getBinaryId()))
		.put("publishedUrl", relativePath)
		.put("content", Base64.encodeBase64String(binaryContent.getContent()))
		.put("publishDate", Calendar.getInstance());
	}
}
package com.tridion.storage.extensions.couchbase.json;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.TimeZone;

import org.apache.commons.codec.binary.Base64;

import com.couchbase.client.java.document.JsonDocument;
import com.couchbase.client.java.document.json.JsonObject;
import com.tridion.storage.BinaryContent;

public class JsonHelper
{

	public static JsonDocument createJson(String id, BinaryContent binaryContent, String relativePath)
	{
        TimeZone tz = TimeZone.getTimeZone("UTC");
        DateFormat df = new SimpleDateFormat("yyyy-MM-dd'T'HH:mmZ");
		df.setTimeZone(tz);
		String tcmid = String.format("tcm:%s-%s-16", binaryContent.getPublicationId(), binaryContent.getBinaryId());
		return JsonDocument.create(id, JsonObject.empty()
                                                 .put("tcmUri", tcmid)
                                                 .put("publishedUrl", relativePath)
                                                 .put("content", Base64.encodeBase64String(binaryContent.getContent()))
                                                 .put("publishDate", df.format(new Date())));
	}
}

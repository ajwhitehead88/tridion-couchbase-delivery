package com.tridion.storage.extensions.couchbase.json;

import java.util.Calendar;

import org.apache.commons.codec.binary.Base64;

import com.eclipsesource.json.JsonObject;
import com.tridion.storage.BinaryContent;

public class JsonHelper
{
	public static String CreateJson(BinaryContent binaryContent, String relativePath)
	{
		JsonObject json = new JsonObject();
		json.set("tcmUri", String.format("tcm:%s-%s-16", binaryContent.getPublicationId(), binaryContent.getBinaryId()));
		json.set("binaryUrl", relativePath);
		json.set("content", Base64.encodeBase64String(binaryContent.getContent()));
		json.set("lastPublished", Calendar.getInstance().toString());

		return json.asString();
	}
}

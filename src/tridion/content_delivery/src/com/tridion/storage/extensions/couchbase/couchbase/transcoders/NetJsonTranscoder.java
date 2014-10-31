/**
 * Copyright (C) 2014 Couchbase, Inc.
 * Copyright (C) 2006-2009 Dustin Sallings
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALING
 * IN THE SOFTWARE.
 */
 
package com.tridion.storage.extensions.couchbase.couchbase.transcoders;
 
import net.spy.memcached.transcoders.BaseSerializingTranscoder;
 
import net.spy.memcached.CachedData;
import net.spy.memcached.transcoders.Transcoder;
import net.spy.memcached.util.StringUtils;
 
/**
 * Transcoder modified to just treat everything as a String.
 */
public class NetJsonTranscoder extends BaseSerializingTranscoder implements
    Transcoder<Object> {
 
    // General flags
    static final int SERIALIZED = 1;
    static final int COMPRESSED = 2;
    static final int NET_JSON_ENCODED = 274;
 
    // Special flags for specially handled types.
    static final int SPECIAL_BOOLEAN = (1 << 8);
    static final int SPECIAL_INT = (2 << 8);
    static final int SPECIAL_LONG = (3 << 8);
    static final int SPECIAL_DATE = (4 << 8);
    static final int SPECIAL_BYTE = (5 << 8);
    static final int SPECIAL_FLOAT = (6 << 8);
    static final int SPECIAL_DOUBLE = (7 << 8);
    static final int SPECIAL_BYTEARRAY = (8 << 8);
  
    /**
     * Get a serializing transcoder with the default max data size.
     */
    public NetJsonTranscoder() {
        this(CachedData.MAX_SIZE);
    }
 
    /**
     * Get a serializing transcoder that specifies the max data size.
     */
    public NetJsonTranscoder(int max) {
        super(max);
    }
 
    @Override
    public boolean asyncDecode(CachedData d) {
        if ((d.getFlags() & COMPRESSED) != 0 || (d.getFlags() & SERIALIZED) != 0) {
            return true;
        }
        return super.asyncDecode(d);
    }
 
    /*
     * (non-Javadoc)
     *
     * @see net.spy.memcached.Transcoder#decode(net.spy.memcached.CachedData)
     */
    public Object decode(CachedData d) {
        try {
            return decodeString(d.getData());
        } catch(Exception ex) {
            throw new RuntimeException("Could not decode JSON string.", ex);
        }
 
    }
 
    /*
     * (non-Javadoc)
     *
     * @see net.spy.memcached.Transcoder#encode(java.lang.Object)
     */
    public CachedData encode(Object o) {
        byte[] b;
        int flags = 0;
        if (o instanceof String) {
            b = encodeString((String) o);
            if (StringUtils.isJsonObject((String) o)) {
                flags |= NET_JSON_ENCODED;
            } else {
                throw new IllegalArgumentException("Item was not a JSON String.");
            }
        } else {
            throw new IllegalArgumentException("Item was not a JSON String.");
        }
        assert b != null;
        return new CachedData(flags, b, getMaxSize());
    }
}
App.Serializer = DS.RESTSerializer.extend({

    includeType: function (type, name) {
        return this.mappingOption(type, name, 'reference');
    },

    addHasMany: function (hash, record, key, relationship) {
        var type = record.constructor,
            name = relationship.key,
            manyArray, includeType;

        includeType = this.includeType(type, name);
        if (includeType !== 'ids') {
            return this._super(hash, record, key, relationship);
        }

        manyArray = Ember.get(record, name);
        hash[key] = manyArray.mapProperty('id');
    }

});

App.Adapter = DS.RESTAdapter.extend({

    namespace: 'api',
    serializer: App.Serializer.create(),

    dirtyRecordsForHasManyChange: function (dirtySet, record, relationship) {
        var includeType = Ember.get(this, 'serializer').includeType(record.constructor, relationship.secondRecordName);
        if (includeType === 'ids') {
            dirtySet.add(record);
        }

        this._super(dirtySet, record, relationship);
    }

});

App.Adapter.map('App.Job', {
    attachments: { include: 'ids' }
});

DS.Model.reopen({
    createdAt: DS.attr('date'),
    updatedAt: DS.attr('date')
});

App.reopen({
    Store: DS.Store.extend({
        revision: 12,
        adapter: App.Adapter.create()
    })
});

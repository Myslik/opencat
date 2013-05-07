App.Serializer = DS.RESTSerializer.extend({

    referenceType: function (type, name) {
        return this.mappingOption(type, name, 'reference');
    },

    addHasMany: function (hash, record, key, relationship) {
        var type = record.constructor,
            name = relationship.key,
            manyArray, referenceType;

        referenceType = this.referenceType(type, name);
        if (referenceType !== 'ids') {
            return this._super(hash, record, key, relationship);
        }

        manyArray = Ember.get(record, name);
        hash[key] = manyArray.mapProperty('id');
    }

});

App.Adapter = DS.RESTAdapter.extend({
    namespace: 'api',
    serializer: App.Serializer.create()
});

App.Adapter.map('App.Job', {
    attachments: { reference: 'ids' }
});

App.reopen({
    Store: DS.Store.extend({
        revision: 12,
        adapter: App.Adapter.create()
    })
});

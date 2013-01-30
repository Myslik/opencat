DS.JSONTransforms.array = {
    serialize: function (deserialized) {
        return deserialized;
    },
    deserialize: function (serialized) {
        return serialized;
    }
}

App.reopen({
    Store: DS.Store.extend({
        revision: 11,
        adapter: DS.RESTAdapter.create({
            namespace: 'api'
        })
    })
});

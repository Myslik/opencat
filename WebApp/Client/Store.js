App.Adapter = DS.RESTAdapter.extend({
    namespace: 'api'
});

App.reopen({
    Store: DS.Store.extend({
        revision: 12,
        adapter: App.Adapter.create()
    })
});

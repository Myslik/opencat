App.DocumentsController = Ember.ArrayController.extend({
    select: function (document) {
        this.get('router').transitionTo('document', document);
    },
    remove: function (document) {
        var store = document.store;
        document.deleteRecord();
        store.commit();
    },
    router: Ember.computed(function () {
        return this.container.lookup('router:main');
    })
});
App.DocumentController = Ember.ObjectController.extend({
    save: function () {
        this.get('content').store.commit();
        this.get('router').transitionTo('documents');
    },
    router: Ember.computed(function () {
        return this.container.lookup('router:main');
    })
});

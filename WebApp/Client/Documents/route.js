App.DocumentRoute = App.AuthRoute.extend({
    events: {
        create: function () {
            this.transitionTo('document.new');
        },
        show: function (document) {
            this.transitionTo('document.edit', document);
        },
        remove: function (document) {
            document.deleteRecord();
            document.transaction.commit();
            this.transitionTo('document');
        }
    },
});

App.DocumentIndexRoute = App.AuthRoute.extend({
    events: {
        reload: function () {
            App.Document.find();
        }
    },
    model: function () {
        return App.Document.all();
    }
});

App.DocumentNewRoute = App.AuthRoute.extend({
    events: {
        update: function (document) {
            document.transaction.commit();
            this.transitionTo('document');
        },
        cancel: function (document) {
            document.deleteRecord();
            this.transitionTo('document');
        }
    },
    model: function () {
        return App.Document.createRecord({ name: 'New Document', words: 0 });
    }
});

App.DocumentEditRoute = App.AuthRoute.extend({
    events: {
        update: function (document) {
            document.transaction.commit();
            this.transitionTo('document');
        },
        cancel: function (document) {
            document.transaction.rollback();
            this.transitionTo('document');
        }
    }
});

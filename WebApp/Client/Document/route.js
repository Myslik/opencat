App.DocumentsRoute = App.AuthRoute.extend({
    events: {
        create: function () {
            this.transitionTo('documents.new');
        },
        show: function (document) {
            this.transitionTo('document', document);
        },
        remove: function (document) {
            document.deleteRecord();
            document.transaction.commit();
            this.transitionTo('documents');
        }
    }
});

App.DocumentsIndexRoute = App.AuthRoute.extend({
    events: {
        reload: function () {
            App.Document.find();
        }
    },
    model: function () {
        return App.Document.all();
    },
    renderTemplate: function () {
        this.render({ into: 'application' });
    }
});

App.DocumentsNewRoute = App.AuthRoute.extend({
    events: {
        update: function (document) {
            document.transaction.commit();
            this.transitionTo('documents');
        },
        cancel: function (document) {
            document.deleteRecord();
            this.transitionTo('documents');
        }
    },
    model: function () {
        return App.Document.createRecord({ name: 'New Document', words: 0 });
    }
});

App.DocumentRoute = App.AuthRoute.extend({
    events: {
        update: function (document) {
            document.transaction.commit();
            this.transitionTo('documents');
        },
        cancel: function (document) {
            document.transaction.rollback();
            this.transitionTo('documents');
        }
    },
    setupController: function(controller, model) {
        controller.set('content', model);
        this.controllerFor('attachments').set('content', model.get('attachments'));
    },
    renderTemplate: function () {
        this.render();
        this.render('attachments', {
            into: 'document',
            outlet: 'attachments',
            controller: this.controllerFor('attachments')
        });
    }
});

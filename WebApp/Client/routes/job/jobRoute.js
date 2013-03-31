App.JobRoute = Ember.Route.extend({
    exit: function () {
        var job = this.get('currentModel');
        job.transaction.rollback();
    },
    setupController: function (controller, model) {
        controller.set('content', model);
        window.model = model;
        this.controllerFor('attachments').set('content', model.get('attachments'));
    },
    renderTemplate: function () {
        this.render();
        this.render('attachments', {
            into: 'job',
            outlet: 'attachments',
            controller: this.controllerFor('attachments')
        });
    }
});

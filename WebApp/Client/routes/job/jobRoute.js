App.JobRoute = Ember.Route.extend({
    exit: function () {
        var job = this.get('currentModel');
        job.transaction.rollback();
    },

    setupController: function (controller, model) {
        controller.set('content', model);
    }
});

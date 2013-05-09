App.JobsIndexRoute = Ember.Route.extend({

    model: function () {
        return App.Job.all();
    }

});

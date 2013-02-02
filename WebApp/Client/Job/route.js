App.JobsRoute = App.AuthRoute.extend({
    events: {
        create: function () {
            this.transitionTo('jobs.new');
        },
        show: function (job) {
            this.transitionTo('job', job);
        },
        remove: function (job) {
            job.deleteRecord();
            job.transaction.commit();
            this.transitionTo('jobs');
        }
    }
});

App.JobsIndexRoute = App.AuthRoute.extend({
    events: {
        reload: function () {
            App.Job.find();
        }
    },
    model: function () {
        return App.Job.all();
    },
    renderTemplate: function () {
        this.render({ into: 'application' });
    }
});

App.JobsNewRoute = App.AuthRoute.extend({
    events: {
        update: function (job) {
            job.transaction.commit();
            this.transitionTo('jobs');
        },
        cancel: function (job) {
            job.deleteRecord();
            this.transitionTo('jobs');
        }
    },
    model: function () {
        return App.Job.createRecord({ name: 'New Job', words: 0 });
    },
    renderTemplate: function () {
        this.render({ into: 'application' });
    }
});

App.JobRoute = App.AuthRoute.extend({
    exit: function () {
        var job = this.get('currentModel');
        job.transaction.rollback();
    },
    setupController: function(controller, model) {
        controller.set('content', model);
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

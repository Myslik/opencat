window.App = Ember.Application.create();

App.ready = function () {
    window.User = App.user = App.User.create();

    App.Language.find();
    App.Job.find();
};

App.ApplicationController = Ember.Controller.extend();
App.ApplicationView = Ember.View.extend({ templateName: 'application', elementId: 'application-container' });

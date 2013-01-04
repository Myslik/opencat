window.App = Ember.Application.create({
    user: Ember.Object.create({
        authenticated: true
    })
});

App.ApplicationController = Ember.Controller.extend();
App.ApplicationView = Ember.View.extend({ templateName: 'application' });

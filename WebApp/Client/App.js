window.App = Ember.Application.create({
    user: Ember.Object.create({
        name: 'Premysl Krajcovic',
        authenticated: true
    })
});

App.ready = function () {
    App.Language.find();
    App.Document.find();
};

App.ApplicationController = Ember.Controller.extend();
App.ApplicationView = Ember.View.extend({ templateName: 'application', elementId: 'application-container' });

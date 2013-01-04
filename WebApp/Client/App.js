(function () {

    "use strict";

    window.App = Ember.Application.create({
        name: 'Ember Upgrade'
    });

    App.ApplicationController = Ember.Controller.extend();
    App.ApplicationView = Ember.View.extend({ templateName: 'application' });

})();

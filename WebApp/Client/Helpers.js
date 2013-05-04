Ember.Handlebars.registerBoundHelper('ago', function (date) {
    return moment(date).fromNow();
});

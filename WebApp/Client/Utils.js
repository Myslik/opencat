var Utils = Utils || {};

Utils.loadTemplates = function (templates, callback) {
    var deferreds = [];
    _.each(templates, function (template) {
        deferreds.push($.get('/Client/Templates/' + template + '.html', function (data) {
            Ember.TEMPLATES[template] = Ember.Handlebars.compile(data);
        }));
    })
    $.when.apply(null, deferreds).done(callback);
};

window.Utils = Utils;

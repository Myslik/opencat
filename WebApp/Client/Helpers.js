var handlebarsGet = Ember.Handlebars.get, normalizePath = Ember.Handlebars.normalizePath;

Handlebars.registerHelper('ago', function (property, options) {
    var context = (options.contexts && options.contexts[0]) || this,
        normalized = normalizePath(context, property, options.data),
        pathRoot = normalized.root,
        path = normalized.path,
        value = (path === 'this') ? pathRoot : handlebarsGet(pathRoot, path, options);
    return moment(value).fromNow();
});

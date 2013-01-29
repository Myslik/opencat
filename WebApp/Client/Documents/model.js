var attr = DS.attr;

App.Document = DS.Model.extend({
    name: attr('string', { required: true }),
    words: attr('number')
});

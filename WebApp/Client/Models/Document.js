var attr = DS.attr;

App.Document = DS.Model.extend({
    name: attr('string'),
    words: attr('number')
});
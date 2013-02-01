var attr = DS.attr;

App.Document = DS.Model.extend({
    // Attributes
    createdAt: attr('date'),
    name: attr('string'),
    words: attr('number'),
    attachments: attr('array'),

    // Relationships
    attachments: DS.hasMany('App.Attachment')
});

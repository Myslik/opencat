var attr = DS.attr;

App.Job = DS.Model.extend({
    // Attributes
    name: attr('string'),
    description: attr('string'),
    words: attr('number'),

    // Relationships
    attachments: DS.hasMany('App.Attachment')
});

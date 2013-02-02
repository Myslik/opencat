var attr = DS.attr;

App.Job = DS.Model.extend({
    // Attributes
    createdAt: attr('date'),
    name: attr('string'),
    description: attr('string'),
    words: attr('number'),
    attachments: attr('array'),

    // Relationships
    attachments: DS.hasMany('App.Attachment', { key: 'attachments' })
});

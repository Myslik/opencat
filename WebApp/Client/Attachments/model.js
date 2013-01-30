var attr = DS.attr;

App.Attachment = DS.Model.extend({
    // Attributes
    name: attr('string'),
    uploadedAt: attr('date'),
    size: attr('number'),
    md5: attr('string'),
    contentType: attr('string'),

    // Relationships
    document: DS.belongsTo('App.Document')
});

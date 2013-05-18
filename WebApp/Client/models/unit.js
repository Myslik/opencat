var attr = DS.attr;

App.Unit = DS.Model.extend({
    // Attributes
    source: attr('string'),
    target: attr('string'),

    // Relationships
    attachment: DS.belongsTo('App.Attachment'),
    job: DS.belongsTo('App.Job')
});

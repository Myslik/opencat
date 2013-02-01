App.AttachmentsView = Ember.CollectionView.extend({
    tagName: 'ul',
    classNames: ['unstyled'],
    attributeBindings: ['style'],
    style: 'margin-left: 180px;',
    contentBinding: 'controller',
    itemViewClass: Ember.View.extend({
        templateName: 'attachment/item',
    })
});

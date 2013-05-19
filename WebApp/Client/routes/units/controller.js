App.UnitsController = Ember.ArrayController.extend({
    save: function (unit) {
        unit.save();
    },
    redo: function (unit) {
        unit.rollback();
    }
});

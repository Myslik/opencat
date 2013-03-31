App.Router.map(function () {
    this.route('index', { path: '/' });
    this.resource('jobs', function () {
        this.route('new');
    });
    this.resource('job', { path: '/jobs/:job_id' });
    this.route('api');
});

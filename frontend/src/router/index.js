import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

const routes = [
    {
        name: 'List',
        path: '/',
        component: () => import('../components/List.vue'),
    },
    {
        name: 'Form',
        path: '/form/:templateId/:id',
        component: () => import('../components/CreateForm.vue'),
        props: true,
    },
    {
        name: 'Form',
        path: '/form/:templateId',
        component: () => import('../components/CreateForm.vue'),
        props: true,
    },
];

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes,
});

export default router;
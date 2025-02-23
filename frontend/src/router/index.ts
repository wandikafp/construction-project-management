import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import ProjectList from '../views/ProjectList.vue';
import ProjectDetail from '../views/ProjectDetail.vue';
import LoginPage from '@/views/LoginPage.vue';
import ProjectForm from '@/views/ProjectForm.vue';

const routes = [
  { path: "/", component: Home },
  { path: "/projects", name: 'ProjectList', component: ProjectList },
  { path: "/projects/:id", name: 'ProjectDetail', component: ProjectDetail, props: true },
  { path: "/project-form/:id?", name: 'ProjectForm', component: ProjectForm, props: true },
  { path: "/login", component: LoginPage },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
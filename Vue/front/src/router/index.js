import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../Views/HomeView.vue'
import AvisosView from '../Views/AvisosView.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: HomeView },
    { path: '/avisos', component: AvisosView },
    { path: '/avisos/:id', component: AvisosView }
  ]
})

export default router
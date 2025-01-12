import { createWebHistory, createRouter } from 'vue-router'

import HomeGeneric from './components/HomeGeneric.vue'
import AboutComponent from './components/AboutComponent.vue'
import FullComponent from './components/FullComponent.vue'
import { AuthenticationMiddleware } from './middlewares/AuthenticationMiddleware'
import { AccessType } from './models/user'

const routes = [
  { path: '/', component: HomeGeneric },
  { path: '/about', component: AboutComponent },
  { path: '/full', component: FullComponent, beforeEnter: (to, from, next) => { return authenticationMiddleware.guardAccess(AccessType.full, next) } }
]

const authenticationMiddleware = new AuthenticationMiddleware();

const router = createRouter({
  history: createWebHistory(),
  routes,
})

router.beforeEach((to, from, next) => { authenticationMiddleware.authenticate(to, from, next)});

export default router

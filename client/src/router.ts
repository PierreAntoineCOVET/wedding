import { createWebHistory, createRouter, type NavigationGuardNext, type RouteLocationNormalized, type RouteLocationNormalizedLoaded } from 'vue-router'

import HomeComponent from './components/HomeComponent.vue'
import Schedule from './components/ScheduleComponent.vue'
import Info from './components/InfoComponent.vue'
import Form from './components/FormComponent.vue'
import Contact from './components/ContactComponent.vue'
import Admin from './components/AdminComponent.vue'
import PageNotFound from './components/NotFound.vue'

import { AuthenticationMiddleware } from './middlewares/AuthenticationMiddleware'
import { Roles } from './models/User'

const routes = [
  { path: '/', component: HomeComponent },
  { path: '/schedule', component: Schedule },
  { path: '/info', component: Info },
  { path: '/form', component: Form },
  { path: '/Contact', component: Contact },
  {
    path: '/admin',
    component: Admin,
    beforeEnter: (to: RouteLocationNormalized, from: RouteLocationNormalizedLoaded, next: NavigationGuardNext) =>
      { return authenticationMiddleware.guardAccess(Roles.admin, next) }
  },

  { path: '/:pathMatch(.*)*', component: PageNotFound }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

const authenticationMiddleware = new AuthenticationMiddleware(router);
router.beforeEach((to, from, next) => { authenticationMiddleware.authenticate(to, from, next)});

export default router

import { createWebHistory, createRouter, type NavigationGuardNext, type RouteLocationNormalized, type RouteLocationNormalizedLoaded } from 'vue-router'

import HomeGeneric from './components/HomeGeneric.vue'
import Schedule from './components/ScheduleComponent.vue'
import Info from './components/InfoComponent.vue'
import Form from './components/FormComponent.vue'
import Admin from './components/AdminComponent.vue'
import PageNotFound from './components/NotFound.vue'

import { AuthenticationMiddleware } from './middlewares/AuthenticationMiddleware'
import { AccessGroup } from './models/user'

const routes = [
  { path: '/', component: HomeGeneric },
  { path: '/schedule', component: Schedule },
  { path: '/info', component: Info },
  { path: '/form', component: Form },
  {
    path: '/admin',
    component: Admin,
    beforeEnter: (to: RouteLocationNormalized, from: RouteLocationNormalizedLoaded, next: NavigationGuardNext) =>
      { return authenticationMiddleware.guardAccess(AccessGroup.admin, next) }
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

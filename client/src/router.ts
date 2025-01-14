import { createWebHistory, createRouter, type NavigationGuardNext, type RouteLocationNormalized, type RouteLocationNormalizedLoaded } from 'vue-router'

import HomeGeneric from './components/HomeGeneric.vue'
import Schedule from './components/ScheduleComponent.vue'
import Info from './components/InfoComponent.vue'
import Form from './components/FormComponent.vue'
import Admin from './components/AdminComponent.vue'

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
  }
]

const authenticationMiddleware = new AuthenticationMiddleware();

const router = createRouter({
  history: createWebHistory(),
  routes,
})

router.beforeEach((to, from, next) => { authenticationMiddleware.authenticate(to, from, next)});

export default router

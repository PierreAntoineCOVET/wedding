import { type RouteLocationNormalized, type RouteLocationNormalizedLoaded, type NavigationGuardNext, type Router } from 'vue-router'

import { UserService } from '../services/UserService'
import { type User, Roles } from '../models/User'

export class AuthenticationMiddleware {
  private userService: UserService;
  private router: Router;

  constructor(router: Router) {
    this.userService = new UserService();
    this.router = router;
  }

  async authenticate(to: RouteLocationNormalized, from: RouteLocationNormalizedLoaded, next: NavigationGuardNext) {
    if (to.query.user) {
      const loggedUserString = localStorage.getItem(UserService.localeStorageKey);

      //let user = null;
      const loggedUserName = loggedUserString
        ? JSON.parse(loggedUserString).name
        : null;

      if (!loggedUserName || loggedUserName !== to.query.user.toString()) {

        const user = await this.userService.authenticate(to.query.user.toString());

        localStorage.setItem(UserService.localeStorageKey, JSON.stringify(user));
      }

      const routeQuery = { ...to.query };
      delete routeQuery.user;

      next({
        path: to.path,
        query: routeQuery,
        replace: true
      });
    }
    else {
      next();
    }
  }

  guardAccess(accesToCheck: Roles, next: NavigationGuardNext) {
    const loggedUserString = localStorage.getItem(UserService.localeStorageKey);

    if (!loggedUserString) {
      next('/');
    }

    const loggedUser = JSON.parse(loggedUserString as string) as User;

    if (loggedUser.role !== accesToCheck) {
      next('/');
    }

    return next();
  }
}

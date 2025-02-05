import { type RouteLocationNormalized, type RouteLocationNormalizedLoaded, type NavigationGuardNext, type Router, useRouter } from 'vue-router'

import { AuthenticationService } from '../services/AuthenticationService'
import { type User, Roles } from '../models/user'

export class AuthenticationMiddleware {
  private authenticationService: AuthenticationService;
  private router: Router;

  constructor(router: Router) {
    this.authenticationService = new AuthenticationService();
    this.router = router;
  }

  async authenticate(to: RouteLocationNormalized, from: RouteLocationNormalizedLoaded, next: NavigationGuardNext) {
    if (to.query.user) {
      const loggedUserString = localStorage.getItem(AuthenticationService.localeStorageKey);

      //let user = null;
      const loggedUserName = loggedUserString
        ? JSON.parse(loggedUserString).name
        : null;

      if (!loggedUserName || loggedUserName !== to.query.user.toString()) {

        const user = await this.authenticationService.authenticate(to.query.user.toString());

        localStorage.setItem(AuthenticationService.localeStorageKey, JSON.stringify(user));
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
    const loggedUserString = localStorage.getItem(AuthenticationService.localeStorageKey);

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

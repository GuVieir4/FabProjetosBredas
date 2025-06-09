import { createRouter, createWebHistory } from 'vue-router'
import HomePageView from '@/views/HomepageView.vue'
import KartView from '@/views/KartView.vue'
import AdminDashboardView from '@/views/AdminDashboardView.vue'
import ProductDetailsView from '@/views/ProductDetailsView.vue'
import ErrorView from '@/views/NotFoundErrorView.vue'
import CategoryPageView from '@/views/CategoryPageView.vue'
import MarcaPageView from '@/views/MarcaPageView.vue'
import ProdutoPageView from '@/views/ProdutoPageView.vue'
import LoginView from '@/views/LoginView.vue'
import { authService } from '@/services/authService.js'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomePageView,
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView,
    },
    {
      path: '/produtos',
      name: 'produtos',
      component: ProdutoPageView,
    },
    {
      path: '/kart',
      name: 'kart',
      component: KartView,
      meta: { requiresAuth: true }
    },
    {
      path: '/adminDashboard',
      name: 'adminDashboard',
      component: AdminDashboardView,
      meta: { requiresAuth: true }
    },
    {
      path: '/productDetails',
      name: 'productDetails',
      component: ProductDetailsView,
    },
    {
      path: '/categoryPage',
      name: 'categoryPage',
      component: CategoryPageView,
    },
    {
      path: '/marcaPage',
      name: 'marcaPage',
      component: MarcaPageView,
    },
    {
      path: '/error',
      name: 'error',
      component: ErrorView,
    },
    {
      path: '/:pathMatch(.*)*',
      redirect: '/error'
    }
  ],
})

// Guard de autenticação
router.beforeEach((to, from, next) => {
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth);
  
  if (requiresAuth && !authService.isAuthenticated()) {
    // Salvar a rota de destino para redirecionar após o login
    next({
      path: '/login',
      query: { redirect: to.fullPath }
    });
  } else {
    next();
  }
});

export default router


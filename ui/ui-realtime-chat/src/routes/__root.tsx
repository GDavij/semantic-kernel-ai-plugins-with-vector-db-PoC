import { Outlet, createRootRouteWithContext } from '@tanstack/react-router'
import { TanStackRouterDevtools } from '@tanstack/react-router-devtools'

import TanStackQueryLayout from '../integrations/tanstack-query/layout.tsx'

import type { QueryClient } from '@tanstack/react-query'
import { BaseLayout } from '@/layouts/base.layout.tsx'

interface MyRouterContext {
  queryClient: QueryClient
}

export const Route = createRootRouteWithContext<MyRouterContext>()({
  component: () => (
    <>

    <BaseLayout>
      <Outlet />
    </BaseLayout>
      <TanStackRouterDevtools />

      <TanStackQueryLayout />
    </>
  ),
})

<template>
  <div class="min-h-screen bg-slate-950 text-slate-100">
    <header class="sticky top-0 z-10 border-b border-slate-800 bg-slate-950/80 backdrop-blur">
      <div class="mx-auto max-w-5xl px-4 py-3 flex items-center justify-between">
        <div class="flex items-center gap-3">
          <div class="h-10 w-10 rounded-2xl bg-blue-600/20 border border-blue-500/30 grid place-items-center">
            <span class="text-blue-200 font-bold">CW</span>
          </div>

          <div>
            <div class="font-semibold leading-tight">CryptoWallet</div>
            <div class="text-xs text-slate-400 leading-tight">UTN • Programación III</div>
          </div>
        </div>

        <nav class="flex items-center gap-2">
          <RouterLink
            to="/transactions"
            class="px-3 py-2 rounded-xl text-sm border border-slate-800 hover:bg-slate-900 hover:border-blue-500/40 transition"
            active-class="bg-blue-600/20 border-blue-500/50 text-blue-200"
          >
            Transacciones
          </RouterLink>

          <RouterLink
            to="/portfolio"
            class="px-3 py-2 rounded-xl text-sm border border-slate-800 hover:bg-slate-900 hover:border-blue-500/40 transition"
            active-class="bg-blue-600/20 border-blue-500/50 text-blue-200"
          >
            Portfolio
          </RouterLink>
        </nav>
      </div>
    </header>

    <main class="mx-auto max-w-5xl px-4 py-6">
      <RouterView />
    </main>

    <footer class="border-t border-slate-800 mt-10">
      <div class="mx-auto max-w-5xl px-4 py-4 text-xs text-slate-500">
        CryptoWallet • Vue 3 + ASP.NET + SQLite • 
      </div>
    </footer>
  </div>
</template>



<script setup>
</script>

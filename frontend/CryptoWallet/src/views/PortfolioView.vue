<template>
  <div class="space-y-6">
    <div class="flex items-center justify-between">
      <h1 class="text-2xl font-semibold">Portfolio</h1>

      <button
        @click="load"
        :disabled="loading"
        class="rounded-lg border border-slate-800 px-3 py-2 text-sm hover:bg-slate-800 transition disabled:opacity-50"
      >
        {{ loading ? "Cargando…" : "Recargar" }}
      </button>
    </div>

    <p v-if="err" class="text-sm text-red-400">{{ err }}</p>


    <div v-if="data" class="grid grid-cols-1 lg:grid-cols-3 gap-6">
      <section class="lg:col-span-1 rounded-2xl border border-slate-800 bg-slate-900/40 p-4 shadow">
        <h2 class="text-lg font-semibold">Total</h2>

        <div class="mt-2 text-2xl font-bold text-blue-300">
          $ {{ formatMoney(data.total) }}
        </div>

        <p class="mt-1 text-xs text-slate-400">
          Composición por valor en ARS
        </p>

        <div class="mt-4">
          <PortfolioPie
            v-if="chartLabels.length > 0"
            :labels="chartLabels"
            :values="chartValues"
          />
          <p v-else class="text-sm text-slate-400 mt-3">No hay datos para graficar.</p>
        </div>
      </section>

      <section class="lg:col-span-2 rounded-2xl border border-slate-800 bg-slate-900/40 p-4 shadow">
        <div class="flex items-center justify-between mb-3">
          <h2 class="text-lg font-semibold">Detalle</h2>

          <span class="text-xs text-slate-400">
            {{ data.items.length }} activos
          </span>
        </div>

        <div class="overflow-x-auto">
          <table class="w-full text-sm border-collapse">
            <thead>
              <tr class="text-left text-slate-400 border-b border-slate-800">
                <th class="py-2">Crypto</th>
                <th>Cantidad</th>
                <th>Valor (ARS)</th>
                <th>Porcentaje (%)</th>
              </tr>
            </thead>

            <tbody>
              <tr
                v-for="i in data.items"
                :key="i.cryptoCode"
                class="border-b border-slate-800 hover:bg-slate-800/40"
              >
                <td class="py-2 font-medium">
                  {{ formatCrypto(i.cryptoCode) }}
                </td>
                <td>{{ i.amount }}</td>
                <td>$ {{ formatMoney(i.money) }}</td>
                <td class="text-slate-300">
                  {{ pct(i.money, data.total) }}%
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <p v-if="data.items.length === 0" class="mt-3 text-sm text-slate-400">
          No hay saldo.
        </p>
      </section>
    </div>
  </div>
</template>


<script setup>
  import { onMounted, ref, computed } from "vue";
  import { getPortfolio } from "../api/portfolio.api.js";
  import PortfolioPie from "../components/PortfolioPie.vue";
  import { formatMoney, formatCrypto } from "../utils/formatters.js";


  const data = ref(null);
  const loading = ref(false);
  const err = ref("");

  const chartLabels = computed(() => (data.value?.items ?? []).map(x => formatCrypto(x.cryptoCode)));
  const chartValues = computed(() => (data.value?.items ?? []).map(x => Number(x.money ?? 0)));

  function pct(money, total) {
  const t = Number(total || 0);
  if (!t) return "0.00";
  return ((Number(money || 0) / t) * 100).toFixed(2);
  } 

  async function load() {
    loading.value = true;
    err.value = "";
    try {
      data.value = await getPortfolio();
    } catch (e) {
      err.value = e.message || "Error cargando portfolio";
    } finally {
      loading.value = false;
    }
  }

  onMounted(load);
</script>

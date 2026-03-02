<template>
  <div class="space-y-6">

    <h1 class="text-2xl font-semibold">Transacciones</h1>

    <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
      
      <section class="lg:col-span-1 rounded-2xl border border-slate-800 bg-slate-900/40 p-4 shadow">
        <h2 class="text-lg font-semibold mb-4">Nueva Transacción</h2>

        <div class="space-y-3">
          <div>
            <label class="text-sm text-slate-400">Crypto</label>
            <select
              v-model="form.cryptoCode"
              class="mt-1 w-full rounded-lg bg-slate-950 border border-slate-800 px-3 py-2  focus:ring-2 focus:ring-blue-500/40"
            >
              <option v-for="c in cryptos" :key="c.code" :value="c.code">
                {{ c.label }}
              </option>
            </select>
          </div>

          <div>
            <label class="text-sm text-slate-400">Acción</label>
            <select
              v-model="form.action"
              class="mt-1 w-full rounded-lg bg-slate-950 border border-slate-800 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500/40"
            >
              <option value="purchase">Compra</option>
              <option value="sale">Venta</option>
            </select>
          </div>
          <div>
            <label class="text-sm text-slate-400">Cantidad</label>
            <input
              v-model.number="form.cryptoAmount"
              type="number"
              :min="minAmount"
              :step="form.cryptoCode === 'usdc' ? 10 : 0.00000001"
              class="mt-1 w-full rounded-lg bg-slate-950 border border-slate-800 px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500/40"
            />

            <p
              v-if="form.cryptoCode === 'usdc' && form.cryptoAmount < minAmount"
              class="mt-1 text-sm text-red-400"
            >
              El mínimo para USDC es $ {{ formatMoney(minAmount) }}
            </p>
          </div>

          <div class="text-sm">
            <span v-if="loadingPrice" class="text-slate-400">Cargando precio…</span>

            <span v-else-if="unitPrice != null" class="text-slate-300">
              Precio: <b class="text-slate-100">$ {{ formatMoney(unitPrice) }}</b>
              <br />
              Total: <b class="text-blue-300">
                $ {{ formatMoney(unitPrice * form.cryptoAmount) }}
              </b>
            </span>

            <span v-else class="text-red-400">
              No se pudo obtener el precio
            </span>
          </div>

          <button
            @click="onCreate"
            :disabled="loading || (form.cryptoCode === 'usdc' && form.cryptoAmount < minAmount)"
            class="mt-2 w-full rounded-xl bg-blue-600 px-4 py-2 font-semibold hover:bg-blue-500 transition disabled:opacity-50"
          >
            {{ loading ? 'Creando…' : 'Crear Transacción' }}
          </button>

          <p v-if="msg" class="text-sm text-emerald-400">{{ msg }}</p>
          <p v-if="err" class="text-sm text-red-400">{{ err }}</p>
        </div>
      </section>

      <section class="lg:col-span-2 rounded-2xl border border-slate-800 bg-slate-900/40 p-4 shadow">
        <div class="flex items-center justify-between mb-3">
          <h2 class="text-lg font-semibold">Historial</h2>
          <button
            @click="load"
            :disabled="loadingList"
            class="rounded-lg border border-slate-800 px-3 py-1 text-sm hover:bg-slate-800 transition disabled:opacity-50"
          >
            {{ loadingList ? 'Cargando…' : 'Recargar' }}
          </button>
        </div>

        <div class="overflow-x-auto">
          <table class="w-full text-sm border-collapse">
            <thead>
              <tr class="text-left text-slate-400 border-b border-slate-800">
                <th class="py-2">ID</th>
                <th>Crypto</th>
                <th>Acción</th>
                <th>Cantidad</th>
                <th>ARS</th>
                <th>Fecha</th>
                <th></th>
              </tr>
            </thead>

            <tbody>
              <tr
                v-for="t in visibleTxs"
                :key="t.id"
                class="border-b border-slate-800 hover:bg-slate-800/40"
              >
                <td class="py-2">{{ t.id }}</td>
                <td>{{ formatCrypto(t.cryptoCode) }}</td>
                <td>{{ formatAction(t.action) }}</td>
                <td>{{ t.cryptoAmount }}</td>
                <td>$ {{ formatMoney(t.money) }}</td>
                <td>{{ formatDateTime(t.dateTime) }}</td>
                <td>
                  <button
                    @click="onDelete(t.id)"
                    class="text-red-400 hover:text-red-300 text-xs"
                  >
                    Eliminar
                  </button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <p v-if="txs.length === 0" class="mt-3 text-sm text-slate-400">
          No hay transacciones
        </p>

        <div class="mt-3 flex gap-2">
          <button
            v-if="!showAll && txs.length > 5"
            @click="showAll = true"
            class="text-sm text-blue-400 hover:underline"
          >
            Mostrar más
          </button>

          <button
            v-else-if="showAll && txs.length > 5"
            @click="showAll = false"
            class="text-sm text-blue-400 hover:underline"
          >
            Mostrar menos
          </button>
        </div>
      </section>
    </div>
  </div>
</template>


<script setup>
    import { onMounted, reactive, ref, computed } from 'vue';
    import { createTransaction, deleteTransaction, getTransactions } from '../api/transactions.api.js';
    import { watch } from 'vue';
    import { getArsPrice } from "../api/prices.api.js";
    import { formatMoney, formatAction, formatCrypto } from '../utils/formatters.js';
    import { getVisibleTransactions } from '../utils/transactions.js';


    const visibleTxs = computed(() => getVisibleTransactions(txs.value, showAll.value, 5));


    const txs = ref([]);
    const loadingList = ref(false);
    

    const loading = ref(false);
    const msg = ref('');
    const err = ref('');

    const showAll = ref(false);

    const minAmount = ref(0.01);

    const form = reactive({
        cryptoCode: 'btc',
        action: 'purchase',
        cryptoAmount: 0.01,
    });

    const unitPrice = ref(null);
    const loadingPrice = ref(false);
    const exchange = ref("binance");

    
    watch(
        () => form.cryptoCode,
        (newCrypto) => {
            if (newCrypto === 'btc') {
                minAmount.value = 0.00000001;
            } else if (newCrypto === 'eth') {
                minAmount.value = 0.0000001;
            } else if (newCrypto === 'usdc') {
                minAmount.value = 5.0;
            }
        },
        { immediate: true }
    )

    const cryptos = [
        { code: 'btc', label: 'Bitcoin (BTC)' },
        { code: 'eth', label: 'Ethereum (ETH)' },
        { code: 'usdc', label: 'USDC (USD Coin)' },
    ];

    

    async function load() {
        loadingList.value = true;
        try{
            txs.value = await getTransactions();
            showAll.value = false;
        } catch (e) {
            console.error(e);
            err.value = e?.message || "Error al cargar las transacciones";
        } finally {
            loadingList.value = false;
        }
    }

    async function onCreate() {
        msg.value = '';
        err.value = '';
        loading.value = true;

        try{
            const payload = {
                cryptoCode: form.cryptoCode,
                action: form.action,
                cryptoAmount: form.cryptoAmount,
            }

            const created = await createTransaction(payload);
            msg.value = `Creada! ID: ${created.id} | money: ${created.money}`;
            await load();
        }catch (e) {
            err.value = e.message || "Error al crear la transacción";
        }finally{   
            loading.value = false;
        }
    }

    async function onDelete(id) {
        if (!confirm(`Eliminar transacción ID ${id}?`)) return;

        try{
            await deleteTransaction(id);
            await load();
        } catch (e) {
            console.error(e);
            alert('Error al eliminar la transacción');
        }
    }


    function formatDateTime(value) {
        if (!value) return '';
        
        const iso = typeof value === 'string' &&  !value.endsWith("Z") ? `${value}Z` : value;

        const d = new Date(iso);

        const date = d.toLocaleDateString("es-AR", { 
            day: '2-digit',
            month: '2-digit',
            year: 'numeric',
        });

        const time = d.toLocaleTimeString("es-AR", {
            hour: '2-digit',
            minute: '2-digit',
            second: '2-digit',
            hour12: false,
        });

        return `${date} ${time}`;  
    }


    async function loadPrice() {
        if (!form.cryptoCode) {
            unitPrice.value = null;
            return;
        }

        const ex = exchange.value || "binance";

        loadingPrice.value = true;
        try {
            const res = await getArsPrice(ex, form.cryptoCode);
            unitPrice.value = Number(res.arsPrice); 
        } catch (e) {
            console.error("Error al obtener precio:", e);
            unitPrice.value = null;
        } finally {
            loadingPrice.value = false;
        }
    }
    watch(
        () => form.cryptoCode,
        () => loadPrice(),
        { immediate: true }
    );

   
    onMounted(load);
</script>
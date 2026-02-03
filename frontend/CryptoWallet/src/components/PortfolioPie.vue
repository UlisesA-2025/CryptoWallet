<template>
  <div style="width: 100%; max-width: 520px; margin: 0 auto;">
    <canvas ref="canvas"></canvas>
  </div>
</template>

<script setup>
import { onBeforeUnmount, onMounted, ref, watch } from "vue";
import { Chart } from "chart.js/auto";

const props = defineProps({
  labels: { type: Array, default: () => [] },   // ["BTC", "ETH", ...]
  values: { type: Array, default: () => [] },   // [moneyARS, moneyARS, ...]
});

const canvas = ref(null);
let chartInstance = null;

function renderChart() {
  if (!canvas.value) return;

  // Si ya existe, lo destruimos y lo recreamos (simple y seguro)
  if (chartInstance) {
    chartInstance.destroy();
    chartInstance = null;
  }

  chartInstance = new Chart(canvas.value, {
    type: "doughnut", // o "pie"
    data: {
      labels: props.labels,
      datasets: [
        {
          data: props.values,
        },
      ],
    },
    options: {
      responsive: true,
      plugins: {
        legend: { position: "bottom" },
        tooltip: {
          callbacks: {
            label: (ctx) => {
              const label = ctx.label || "";
              const value = ctx.parsed || 0;
              return `${label}: $ ${value.toLocaleString("es-AR", {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2,
              })}`;
            },
          },
        },
      },
      cutout: "55%", // hace que se vea “doughnut”
    },
  });
}

onMounted(renderChart);
watch(() => [props.labels, props.values], renderChart, { deep: true });
onBeforeUnmount(() => chartInstance?.destroy());
</script>

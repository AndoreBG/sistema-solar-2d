# 🌞 Sistema Solar 2D

Jogo/simulação 2D de um sistema solar desenvolvido na engine **Unity**.
O projeto foi criado como meio de obtenção de nota para a matéria **Computação Gráfica** no curso de **Jogos Digitais** da **FATEC Americana**.

## 📖 Sobre

Esta é uma simulação interativa do sistema solar em 2D, com astros (Sol, Terra e Lua) que orbitam em torno de um eixo central, incluindo sistemas de **rotação**, **órbita** e **sombras/iluminação**. O projeto explora conceitos de Computação Gráfica como transformações, iluminação 2D e renderização.

## ✨ Funcionalidades

- ☀️ Sistema de rotação em torno de um eixo central (o Sol)
- 🌍 Astros orbitando (Sol, Terra e Lua)
- 🌑 Sistema de sombras e iluminação 2D (URP)
- 🎮 Cena de *playground* para testes e interação

## 🛠️ Tecnologias

- **Unity** (Universal Render Pipeline - URP)
- **C#** — scripts de lógica
- **ShaderLab / HLSL** — shaders e materiais
- **TextMesh Pro** — textos da interface

## 📁 Estrutura do projeto

```
sistema-solar-2d/
├── Assets/
│   ├── Material/        # Materiais dos astros
│   ├── Scenes/          # Cenas do Unity
│   ├── Script/          # Scripts em C# (prototype, final, finale)
│   ├── Settings/        # Configurações de render/projeto
│   ├── Sprites/         # Imagens dos astros
│   └── TextMesh Pro/    # Pacote de fontes/UI
├── BuildReal/           # Build do projeto
├── Packages/            # Dependências do Unity
├── ProjectSettings/     # Configurações do Unity
└── LICENSE
```

## 🚀 Como executar

### Pré-requisitos
- [Unity Hub](https://unity.com/download) com uma versão igual ou superior ao Unity Editor 6.

### Passos
1. Clone o repositório:
   ```bash
   git clone https://github.com/AndoreBG/sistema-solar-2d.git
   ```
2. Abra o **Unity Hub** e clique em **Add** / **Open**, selecionando a pasta do projeto clonado.
3. Aguarde a importação dos *assets* e pacotes.
4. Abra a cena em `Assets/Scenes/` e pressione **Play** ▶️ no editor.

> 💡 Alternativamente, você pode executar diretamente o build disponível na pasta `BuildReal/`.

---

<br>

<p align="center">Feito para a disciplina de Computação Gráfica · FATEC Americana 🎓</p>

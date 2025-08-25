# 🎬 Movie Streaming Server

A self-hosted movie streaming platform built for learning and experimentation.  
It supports transcoding, adaptive streaming (HLS), authentication, and DevOps workflows — all running on a local network.

---

## 🚀 Features
- **Backend (C# / ASP.NET Core)** — REST API for movie management and streaming.
- **Frontend (Angular)** — Modern responsive web interface.
- **Transcoding (FFmpeg)** — Converts media into adaptive HLS streams.
- **Streaming (HLS + Shaka/hls.js)** — Smooth playback with adaptive bitrate.
- **Database (PostgreSQL)** — Stores metadata and user data.
- **Caching & Messaging (Redis / RabbitMQ)** — Improves performance & task management.
- **Storage (MinIO)** — S3-compatible media storage.
- **Authentication (Keycloak / ASP.NET Identity)** — Secure login and role-based access.
- **Containerized Deployment (Kubernetes k3s + Helm)** — Full DevOps setup for local cluster.
- **Observability (Prometheus, Grafana, Loki)** — Metrics, dashboards, and logging.

---

## 📂 Project Structure
```
Root
├── Web\ # Angular frontend
├── Api\ # ASP.NET Core backend
├── Transcoder\ # FFmpeg integration
├── Helm\ # Kubernetes Helm charts
├── Scripts\ # Helper scripts
└── README.md 
```

---

## 🛠️ Tech Stack
- **Backend:** ASP.NET Core (C# 7.3+)
- **Frontend:** Angular + Tailwind
- **Media Processing:** FFmpeg
- **Streaming Protocol:** HLS
- **Database:** PostgreSQL
- **Cache/Messaging:** Redis / RabbitMQ
- **Storage:** MinIO
- **Auth:** Keycloak / ASP.NET Identity
- **Deployment:** Docker + Kubernetes (k3s) + Helm
- **Monitoring:** Prometheus, Grafana, Loki

---

## ⚡ Getting Started

### 1. Clone the repo
```bash
git clone https://github.com/AhmadElhanafy/Movie-Streaming-Server.git
cd moviestreaming
```

### 2. Backend (ASP.NET Core API)
```bash
cd Api
dotnet build
dotnet run
```

### 3. Frontend (Angular)
```bash
cd Web
npm install
ng serve -o
```

### 4. Transcoding
FFmpeg must be installed and available in PATH:
```bash
ffmpeg -version
```

### 5. Deployment
Using Kubernetes (k3s):
```bash
kubectl apply -f Helm/
```

---

## 🧩 Roadmap
- Basic movie upload & playback
- HLS adaptive streaming
- Authentication & user roles
- Redis/RabbitMQ background jobs
- Helm charts for deployment
- Observability with Grafana/Prometheus
- CI/CD with GitHub Actions

---

### 🤝 Contributing

This project is for learning & experimentation, but contributions are welcome.
Feel free to open issues, suggest features, or submit PRs.

---

### 📜 License

MIT License — feel free to use, learn from, and modify.
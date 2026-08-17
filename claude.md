# Smart Wardrobe Organizer — Project Guidelines

## Goal
Build a production-quality portfolio web application called Smart Wardrobe Organizer.
A full-stack web app that lets users digitize their wardrobe, plan outfits, and receive
weather-based outfit suggestions. The app is photo-first — users upload actual photos
of their clothing items and see their wardrobe visually.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | React |
| Backend | .NET 8 Web API (C# only — no Python) |
| Database | Supabase (PostgreSQL) |
| Auth | Supabase Auth |
| File Storage | Supabase Storage (clothing photos) |
| Weather API | OpenWeatherMap (free tier, by city name) |
| Deployment — Frontend | Vercel |
| Deployment — Backend | Railway |

---

## Architecture

```
Frontend (React)
↓
REST API (.NET 8)
↓
Supabase
├── Auth
├── Storage (clothing photos)
└── PostgreSQL
```

---

## Call Routing

**React → Supabase directly:**
- Sign up
- Log in
- Log out
- Photo uploads (clothing item images go straight to Supabase Storage)

**React → .NET backend:**
- All clothing item CRUD (create, read, update, delete)
- All outfit CRUD
- Weather-based suggestions (backend fetches weather, applies logic, returns result)

---

## Database Schema

### clothing_items
| Column | Type | Notes |
|---|---|---|
| id | uuid | Primary key |
| user_id | uuid | Foreign key → auth.users |
| photo_url | text | Returned from Supabase Storage on upload |
| category | text | top, bottom, shoes, accessory |
| color | text | Free text |
| style | text | casual, formal, sporty |
| season | text | summer, winter, spring/fall |
| created_at | timestamp | |

### outfits
| Column | Type | Notes |
|---|---|---|
| id | uuid | Primary key |
| user_id | uuid | Foreign key → auth.users |
| name | text | User-given outfit name |
| created_at | timestamp | |

### outfit_items
| Column | Type | Notes |
|---|---|---|
| id | uuid | Primary key |
| outfit_id | uuid | Foreign key → outfits |
| clothing_item_id | uuid | Foreign key → clothing_items |

---

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| POST | `/clothing` | Add a clothing item (metadata + photo URL) |
| GET | `/clothing` | Get all items for the logged-in user |
| PUT | `/clothing/:id` | Edit a clothing item |
| DELETE | `/clothing/:id` | Delete a clothing item |
| POST | `/outfits` | Save an outfit with its clothing items |
| GET | `/outfits` | Get all saved outfits for the logged-in user |
| DELETE | `/outfits/:id` | Delete a saved outfit |
| GET | `/suggestions?city=` | Get a weather-based outfit suggestion |

---

## Outfit Suggestion Logic
Rule-based only — no AI in v1.

1. Receive city name from frontend
2. Call OpenWeatherMap API for current weather
3. Map temperature to season tag:
   - Hot → summer
   - Cold → winter
   - Mild → spring/fall
4. Filter user's clothing items by that season tag
5. Return a balanced outfit (minimum: one top, one bottom, one shoes)

---

## MVP Features (v1 scope only)
- Wardrobe inventory management
- Outfit planning
- Weather-based outfit suggestions

**Out of scope for v1 — do not build:**
- Sustainability tracker
- Calendar sync
- AI auto-tagging of photos
- Usage analytics dashboard

---

## Design System

### Colors
- Define a primary, secondary, and neutral color palette
- Include light/dark variants for each
- All colors stored as CSS variables in a global stylesheet

### Typography
- One heading font, one body font
- Define a type scale: h1, h2, h3, body, caption, label
- Font sizes stored as CSS variables

### Spacing
- Use an 8pt spacing scale: 4, 8, 16, 24, 32, 48, 64px
- All spacing values stored as CSS variables

### Layout Grid
- 12-column grid for desktop
- 4-column grid for mobile
- Consistent max-width container with horizontal padding

### Core Components
- Button (primary, secondary, danger variants)
- Input field (text, select, file upload)
- Card (clothing item card, outfit card)
- Navigation bar
- Photo grid
- Modal / drawer
- Loading spinner
- Error message
- Empty state

---

## Repo Structure

```
smart-wardrobe-organizer/
├── client/              ← React app
│   ├── src/
│   │   ├── components/  ← reusable UI components
│   │   ├── pages/       ← one folder per page
│   │   ├── styles/      ← global styles, design tokens
│   │   └── lib/         ← Supabase client, API helpers
├── server/              ← .NET 8 Web API
│   ├── Controllers/
│   ├── Models/
│   └── Services/
├── CLAUDE.md            ← this file
├── README.md
└── .gitignore
```

---

## Branching Strategy

- `main` — production only, never commit directly here
- `dev` — working branch, all features merge here via pull request
- Feature branches off `dev`:
  - `feature/auth`
  - `feature/wardrobe-crud`
  - `feature/outfit-planning`
  - `feature/weather-suggestions`
  - `feature/design-system`

---

## Team & Work Split

| Developer | Owns |
|---|---|
| Natanielle | Phase 3 — Wardrobe inventory |
| Flo | Phases 4 & 5 — Outfit planning + weather suggestions |
| Both | Setup, auth, design system, polish & deploy |

---

## Coding Standards
- Explain every architectural decision before implementing
- One ticket at a time — do not touch unrelated files
- No new libraries without explaining why first
- Follow REST API conventions
- Clean, readable code over clever code
- Ask before making major architectural changes
- Write clear comments only where they add value
- Keep commits focused on a single ticket

---

## Collaboration Rules
- Two developers working concurrently
- Work is divided by feature, not by layer
- Keep changes scoped to assigned tickets to avoid merge conflicts
- Every feature goes through a pull request — no direct pushes to `dev` or `main`
- Review each other's PRs before merging

---

## Claude Code Instructions
- Never generate more than one ticket's worth of code at a time
- Explain every decision before implementing it
- Wait for approval before moving to the next step
- Flag potential bugs, security issues, and performance concerns
- Suggest industry best practices where relevant
- Briefly explain unfamiliar concepts when they come up
- If you see a better design than what's specified, explain why before changing anything
- Do not introduce new libraries without justification
- Identify which files will change before touching them

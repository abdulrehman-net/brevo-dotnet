# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.2.0-alpha] - 2026-05-14

### Added
- **Abdul.Brevo.Core**: New SDK covering Contacts, Lists, Folders, Webhooks, and Account API endpoints.
- **Abdul.Brevo.Abstractions**: New foundational package for shared SDK logic.
- Automated rate-limit header parsing and `BrevoRateLimitException` (429 handling).
- Shared pagination primitives for `limit` and `offset` based endpoints.
- Structured `BrevoApiException` hierarchy with machine-readable error codes.
- Shared `AddBrevoHttpClient` DI extension to reduce registration boilerplate.

### Changed
- Refactored `Abdul.Brevo.Email`, `Abdul.Brevo.Conversations`, and `Abdul.Brevo.Crm` to inherit from the Abstractions layer.
- Consolidated JSON serialization settings and error response parsing.
- Unified `BrevoOptionsBase` configuration across all modules.

## [0.1.0-alpha] - 2026-05-13

### Added
- Initial SDK skeleton and structural setup.
- Core options and HttpClient wrapper.
- Messages client and automated messages client.
- Status and visitors client.
- Basic DTOs for the supported API surfaces.

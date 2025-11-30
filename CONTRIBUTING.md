# Contributing to MediCore EMR

Thank you for your interest in contributing to MediCore EMR!

## Development Workflow

### 1. Fork and Clone
```bash
git clone https://github.com/vaibhaviimcal-web/medicore-emr.git
cd medicore-emr
```

### 2. Create a Branch
```bash
git checkout -b feature/your-feature-name
```

### 3. Make Changes
- Follow the existing code style
- Write clear commit messages
- Test your changes thoroughly

### 4. Commit
```bash
git add .
git commit -m "Add: Brief description of your changes"
```

### 5. Push and Create PR
```bash
git push origin feature/your-feature-name
```

Then create a Pull Request on GitHub.

## Code Style Guidelines

### Backend (C#)
- Follow Microsoft C# coding conventions
- Use meaningful variable names
- Add XML documentation comments for public methods
- Keep methods focused and small

### Frontend (React)
- Use functional components with hooks
- Follow React best practices
- Use meaningful component names
- Keep components small and reusable

### CSS
- Use CSS variables for colors and spacing
- Follow BEM naming convention
- Keep styles modular

## Testing

### Backend
```bash
cd backend
dotnet test
```

### Frontend
```bash
cd frontend
npm test
```

## Pull Request Guidelines

- Provide a clear description of changes
- Reference any related issues
- Ensure all tests pass
- Update documentation if needed

## Reporting Issues

When reporting issues, please include:
- Clear description of the problem
- Steps to reproduce
- Expected vs actual behavior
- Screenshots if applicable
- Environment details (OS, browser, etc.)

## Feature Requests

We welcome feature requests! Please:
- Check if the feature already exists
- Provide a clear use case
- Explain the expected behavior
- Consider implementation complexity

## Code of Conduct

- Be respectful and professional
- Welcome newcomers
- Focus on constructive feedback
- Help others learn and grow

## Questions?

Feel free to open an issue for any questions or clarifications.

Thank you for contributing! 🎉
